import { useContext, useEffect, useState } from 'react';
import { Client, PostFileLookupDto, PostImageLookupDto, PostLookupDto, PostVm } from '../api/api';
import { useParams } from 'react-router-dom';
import { AuthContext } from '../auth/auth-provider';
import Post from './Post';

const apiClient = new Client('https://localhost:44399');

type PostWithFilesAndImages = {
    postDto: PostLookupDto;
    files: PostFileLookupDto[];
    images: PostImageLookupDto[];
};

function PostByID(props: any) {
    const [post, setPost] = useState<PostWithFilesAndImages>();

    const { role } = useContext(AuthContext);

    const params = useParams();
    const id = params.id;

    useEffect(() => {
        async function getPost() {
            const postDetailsVm = await apiClient.getPost(id!, '1.0');

            let postFileListVm = await apiClient.getAllPostFiles(postDetailsVm.id!, '1.0');
            let postImageListVm = await apiClient.getAllPostImages(postDetailsVm.id!, '1.0');
        
            const post: PostWithFilesAndImages = {
                postDto: postDetailsVm,
                files: postFileListVm.files != undefined ? postFileListVm.files : [],
                images: postImageListVm.images != undefined ? postImageListVm.images : []
            };

            setPost(post);
        }

        getPost();
    }, []);

    return (
        <>
            {
                post !== undefined
                ?
                <Post post={post} role={role} setPostId={props.setPostId} isAllCommentsVisible={true}/>
                :
                <></>
            }
        </>
        
    );
};
export default PostByID;