import { Client, PostFileLookupDto, PostImageLookupDto } from '../api/api'; 
import PostFooter from './PostFooter';
import DeleteButton from '../images/delete.png';
import EditButton from '../images/edit.png';
import EmptyPreview from '../images/empty_post_preview.png';
import { useEffect, useState } from 'react';
import FileImg from '../images/fileW.png'
import NextA from '../images/nexta.png'
import NextI from '../images/nexti.png'
import BackA from '../images/backa.png'
import BackI from '../images/backi.png'
import { useNavigate } from 'react-router-dom';

const apiClient = new Client(process.env.REACT_APP_SERVER_URL);

function Post(props: any) {
    const [imageURL, setImageURL] = useState(props.post.preview);
    const [fileList, setFileList] = useState<PostFileLookupDto[]>(props.post.files);
    const [imageList, setImageList] = useState<PostImageLookupDto[]>(props.post.images);
    const [currentId, setCurrentId] = useState(0);
    const [leftButtonImage, setLeftButtonImage] = useState(BackI);
    const [rightButtonImage, setRightButtonImage] = useState(NextI);

    const navigate = useNavigate();
    
    const handleDownload = (file: PostFileLookupDto) => {
        const byteCharacters = atob(file.base64Code!);
        const byteArrays = [];

        for (let offset = 0; offset < byteCharacters.length; offset += 512) {
            const slice = byteCharacters.slice(offset, offset + 512);

            const byteNumbers = new Array(slice.length);
            for (let i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            const byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }

        const blob = new Blob(byteArrays, { type: file.fileType });
        const url = URL.createObjectURL(blob);

        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', file.name!);
        document.body.appendChild(link);
        link.click();

        document.body.removeChild(link);
        URL.revokeObjectURL(url);
    };
    
    const handleOnClickBack = () => {
        if (currentId !== 0) {
            if (currentId === 1) {
                setLeftButtonImage(BackI);
            }

            setImageURL(`data:${imageList[currentId - 1].fileType!};base64,${imageList[currentId - 1].base64Code!}`);
            setCurrentId(currentId - 1);
            setRightButtonImage(NextA);
        }
    };

    const handleOnClickNext = () => {
        if (currentId !== imageList.length - 1) {
            if (currentId === imageList.length - 2) {
                setRightButtonImage(NextI);
            }

            setImageURL(`data:${imageList[currentId + 1].fileType!};base64,${imageList[currentId + 1].base64Code!}`);
            setCurrentId(currentId + 1);
            setLeftButtonImage(BackA);
        }
    };

    async function deletePost(postId: string) {
        await apiClient.deletePost(postId, '1.0');
        
        window.location.href = '/index';
    }

    async function editPost(postId: string) {
        props.setPostId(postId);

        navigate('/edit-post');
    }

    useEffect(() => {
        if (imageList.length > 1) {
            setRightButtonImage(NextA);
        }
    }, []);

    return (
        <div className='post' key={props.post.postDto.id}>
                
            <div className='post_title_container'>
                <ul>
                    <a href={`/posts/${props.post.postDto.id}`}><li className='post_title'>Скоро в школу!</li></a>
                </ul>
                {
                    props.role === 'Owner' || props.role === 'Admin'
                    ?
                    <div className='post_buttons_container'>
                        <input type='image' className='post_edit_button' src={EditButton} onClick={() => editPost(props.post.postDto.id!)} alt="EditButton"/>
                        <input type='image' className='post_delete_button' src={DeleteButton} onClick={() => deletePost(props.post.postDto.id!)} alt="DeleteButton"/>
                    </div>
                    :
                    <></>
                }
                        
            </div>

            <div className='post_date'>{props.post.postDto.creationDate}</div>

            {
                imageList.length > 0
                ?
                    <div className='post_image_block'>
                        <img
                            src={imageURL !== '' ? imageURL : EmptyPreview}
                            alt='post preview'
                            className='post_image_preview'
                        />
                        <button
                                    id='left_arrow'
                                    type='button'
                                    className='post_image_left_button'
                                    onClick={handleOnClickBack}
                        >
                            <img
                                src={leftButtonImage}
                                alt='post arrow'
                                className='post_image_arrow'
                            />
                        </button>

                        <button
                                    id='right_arrow'
                                    type='button'
                                    className='post_image_right_button'
                                    onClick={handleOnClickNext}
                        >
                            <img
                                src={rightButtonImage}
                                alt='post arrow'
                                className='post_image_arrow'
                            />
                        </button>
                        
                    </div>
                    
                :
                    <img
                        src={EmptyPreview}
                        alt='post preview empty'
                        className='post_image_preview_empty'
                    />
            }
                            
            <div className='post_text'>{props.post.postDto.text}</div>

            {
                fileList.length > 0
                ?
                    <div className='post_files_panel'>
                        {fileList!.map((file: PostFileLookupDto) => (
                            <button
                                className='download_file_button'
                                type='button'
                                key={file.name}
                                onClick={() => handleDownload(file)}>
                                <div className='file_container'>
                                    <img className='file_icon' alt='file icon' src={FileImg}/>
                                    <div className='file_name'><p className='file_name_text'>{file.name}</p></div>
                                </div>
                            </button>
                        ))}
                    </div>
                :
                    <div className='post_files_panel_empty'/>
            }
                            
            <PostFooter postId={props.post.postDto.id} isAllCommentsVisible={props.isAllCommentsVisible}/>
                          
        </div>
    );
};
export default Post;