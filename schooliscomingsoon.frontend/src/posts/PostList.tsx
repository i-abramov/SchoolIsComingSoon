import { useContext, useEffect, useState } from 'react';
import { Client, PostFileLookupDto, PostImageLookupDto, PostLookupDto } from '../api/api';
import { useLocation } from 'react-router-dom';
import { SearchContext } from '../header/search-provider';
import { AuthContext } from '../auth/auth-provider';
import PostWithSub from './PostWithSub';

const apiClient = new Client(process.env.REACT_APP_SERVER_URL);

type PostWithFilesAndImages = {
    postDto: PostLookupDto;
    files: PostFileLookupDto[];
    images: PostImageLookupDto[];
    preview: string;
};

function PostList(props: any) {
    const [posts, setPosts] = useState<PostWithFilesAndImages[]>([]);
    const { text } = useContext(SearchContext);
    const { role } = useContext(AuthContext);
    const location = useLocation();

    const getFilteredPosts = (allPosts: PostLookupDto[]) => {
        switch (location.pathname) {
            case '/pre-school-education':
                return allPosts.filter(p => p.categories?.includes('Дошкольное образование'));
            case '/elementary-grades':
                return allPosts.filter(p => p.categories?.includes('Начальные классы'));
            case '/information-for-parents':
                return allPosts.filter(p => p.categories?.includes('Информация для родителей'));
            default:
                return allPosts;
        }
    };

    const fetchPosts = async () => {
        const postListVm = await apiClient.getAllPosts('1.0');
        const filteredList = getFilteredPosts(postListVm.posts ?? []);

        const postsWithExtras = await Promise.all(
            filteredList.map(async post => {
                const postFileListVm = await apiClient.getAllPostFiles(post.id!, '1.0');
                const postImageListVm = await apiClient.getAllPostImages(post.id!, '1.0');

                const images = postImageListVm.images ?? [];
                const preview =
                    images.length > 0 ? `data:image/png;base64,${images[0].base64Code!}` : '';

                return {
                    postDto: post,
                    files: postFileListVm.files ?? [],
                    images,
                    preview,
                };
            })
        );

        setPosts(postsWithExtras);
    };

    useEffect(() => {
        const filterOrFetch = async () => {
            if (text.trim() !== '') {
                setPosts(prev =>
                    prev.filter(p =>
                        p.postDto.text?.toLowerCase().includes(text.toLowerCase())
                    )
                );
            } else {
                await fetchPosts();
            }
        };
        filterOrFetch();
    }, [text, location.pathname]);

    return (
        <>
            {posts.map(post => (
                <PostWithSub
                    key={post.postDto.id}
                    post={post}
                    role={role}
                    setPostId={props.setPostId}
                    isAllCommentsVisible={false}
                />
            ))}
        </>
    );
}

export default PostList;