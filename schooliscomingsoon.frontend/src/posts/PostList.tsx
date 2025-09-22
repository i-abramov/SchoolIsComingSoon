import { useContext, useEffect, useState } from 'react';
import { Client, PostFileLookupDto, PostImageLookupDto, PostLookupDto } from '../api/api';
import { useLocation } from 'react-router-dom';
import { SearchContext } from '../header/search-provider';
import { AuthContext } from '../auth/auth-provider';
import Post from './Post';
import PostWithSub from './PostWithSub';

const apiClient = new Client('https://localhost:44399');

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

    useEffect(() => {
        const getPosts = async () => {
            const postListVm = await apiClient.getAllPosts('1.0');
            let filteredList: PostLookupDto[] | undefined;
        
            if (location.pathname === '/pre-school-education') {
                filteredList = postListVm.posts?.filter(post => post.categories?.includes('Дошкольное образование'));
            } else if (location.pathname === '/elementary-grades') {
                filteredList = postListVm.posts?.filter(post => post.categories?.includes('Начальные классы'));
            }  else if (location.pathname === '/information-for-parents') {
                filteredList = postListVm.posts?.filter(post => post.categories?.includes('Информация для родителей'));
            } else {
                filteredList = postListVm.posts;
            }

            let postsWithFilesAndImages: PostWithFilesAndImages[] = [];

            if (filteredList != undefined) {
                for (let i = 0; i < filteredList.length; i++) {
                    let postFileListVm = await apiClient.getAllPostFiles(filteredList[i].id!, '1.0');
                    let postImageListVm = await apiClient.getAllPostImages(filteredList[i].id!, '1.0');

                    const post: PostWithFilesAndImages = {
                        postDto: filteredList[i],
                        files: postFileListVm.files != undefined ? postFileListVm.files : [],
                        images: postImageListVm.images != undefined ? postImageListVm.images : [],
                        preview: postImageListVm.images != undefined && postImageListVm.images.length > 0 ? `data:image/png;base64,${postImageListVm.images[0].base64Code!}` : ''
                    };

                    postsWithFilesAndImages.push(post);
                }
            }
            

            setPosts(postsWithFilesAndImages);
        }

        if (text !== '') {
            var filteredList = posts?.filter(function(postWithFilesAndImages) {
                return postWithFilesAndImages.postDto.text?.toLowerCase().search(text.toLowerCase()) !== -1;
            }); 
            setPosts(filteredList);
        } else {
            getPosts();
        }
    }, [text]);

    return (
        <>
            {posts?.map((post) => (
                <PostWithSub post={post} role={role} setPostId={props.setPostId} isAllCommentsVisible={false}/>
            ))}
        </>
    );
};
export default PostList;