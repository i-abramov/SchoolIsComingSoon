import { useEffect, useState } from 'react';
import PostEditorFilePanel from './PostEditorFilePanel';
import { Client, SubscriptionListVm, SubscriptionLookupDto } from '../../api/api';
import PostEditorTagPanel from './PostEditorTagPanel';
import PostEditorTextArea from './PostEditorTextarea';
import PostEditorToolbar from './PostEditorToolbar';
import PostEditorImagePanel from './PostEditorImagePanel';
import PostEditorToggleSwitchPanel from './PostEditorToggleSwitchPanel';

export type FileData = {
    name: string;
    base64: string;
    url: string;
    fileType: string;
    id: number;
};

export type FormData = {
    subscriptionId: string;
    text: string;
    files: FileData[];
    images: FileData[];
    categories: string;
}

type FormInputs = {
    text: HTMLInputElement;
    images: HTMLInputElement;
    files: HTMLInputElement;
}

interface PostProps {
    onSubmit: (data: FormData) => void;
    postId: string;
}

const apiClient = new Client('https://localhost:44399');

const filesFromFileReader: FileData[] = [];

export default function PostEditorPage({ onSubmit, postId }: PostProps) {
    const [inputText, setInputText] = useState('');
    const [fileList, setFileList] = useState<FileData[]>([]);
    const [imageList, setImageList] = useState<FileData[]>([]);
    const [categories, setCategories] = useState('');
    const [subscriptions, setSubscriptions] = useState<SubscriptionLookupDto[]>([]);
    const [subscriptionId, setSubscriptionId] = useState('');

    const fileReader = new FileReader();
    
    const readFiles = (files: any) => {
        if (files && files.length) {
            if (files[0].type == 'image/png' || files[0].type == 'image/jpeg' || files[0].type == 'image/bmp') {
                if (imageList.length == 5) {
                    alert('Превышен лимит фотографий для поста!');
                    return;
                }
            }
            else {
                if (fileList.length == 5) {
                    alert('Превышен лимит файлов для поста!');
                    return;
                }
            }
            addNewFile(files[0].name, '', '', files[0].type);
            fileReader.readAsDataURL(files[0]);
        }
    }
    
    fileReader.onloadend = () => {
        let url = fileReader.result!.toString();
        filesFromFileReader[filesFromFileReader.length - 1].base64 = url.split('base64,')[1];
        filesFromFileReader[filesFromFileReader.length - 1].url = url;
        filesFromFileReader[filesFromFileReader.length - 1].fileType = url.split(';base64,')[0].replace('data:', '');
    
        let files: FileData[] = [];
        let images: FileData[] = [];
    
        for (let i = 0; i < filesFromFileReader.length; i++) {
            if (filesFromFileReader[i].fileType == 'image/png' || filesFromFileReader[i].fileType == 'image/jpeg' || filesFromFileReader[i].fileType == 'image/bmp') {
                images.push(filesFromFileReader[i]);
            }
            else {
                files.push(filesFromFileReader[i]);
            }
        }
    
        setFileList(files);
        setImageList(images);
    };
    
    const addNewFile = (name: string, base64: string, url: string, fileType: string) => {
        let ID = filesFromFileReader.length > 0 ? filesFromFileReader[filesFromFileReader.length - 1].id + 1 : 0;
        filesFromFileReader.push({
            name: name,
            base64: base64,
            url: url,
            fileType: fileType,
            id: ID
        });
    }
    
    const removeImage = (id: number) => {
        let images: FileData[] = [];
                    
        let index = filesFromFileReader.findIndex(file => file.id == id);
        filesFromFileReader.splice(index, 1);
            
        for (let i = 0; i < filesFromFileReader.length; i++) {
            if (filesFromFileReader[i].fileType == 'image/png' || filesFromFileReader[i].fileType == 'image/jpeg'  || filesFromFileReader[i].fileType == 'image/bmp') {
                images.push(filesFromFileReader[i]);
            }
        }
            
        setImageList(images);
    }
    
    const removeFile = (id: number) => {
        let files: FileData[] = [];
            
        let index = filesFromFileReader.findIndex(file => file.id == id);
        filesFromFileReader.splice(index, 1);
    
        for (let i = 0; i < filesFromFileReader.length; i++) {
            if (filesFromFileReader[i].fileType != 'image/png' && filesFromFileReader[i].fileType != 'image/jpeg' && filesFromFileReader[i].fileType != 'image/bmp') {
                files.push(filesFromFileReader[i]);
            }
        }
    
        setFileList(files);
    }

    const handleOnClickCancel = () => {
        window.location.href = '/index';
    }

    const handleSubmit: React.FormEventHandler<HTMLFormElement & FormInputs> = (event) => {
        event.preventDefault();
        const form = event.currentTarget;
        const { text } = form;

        if (text.value == '') {
            alert('Введите текст для создания поста!');
        }
        else {

            onSubmit({
                subscriptionId: subscriptionId,
                text: text.value,
                files: fileList,
                images: imageList,
                categories: categories
            });
        }
    }

    useEffect(() => {
        async function getPostData() {
            const post = await apiClient.getPost(postId, '1.0');
    
            let categories = '';
            if (post.categories != undefined) {
                for (let i = 0; i < post.categories.length; i++) {
                    categories += post.categories[i] + '\n';
                }
                setCategories(categories);
            }
    
            const postFiles = await apiClient.getAllPostFiles(postId, '1.0');
    
            let files: FileData[] = [];
    
            if (postFiles.files !== undefined && postFiles.files.length > 0) {
    
                for (let i = 0; i < postFiles.files.length; i++) {
                    let file = postFiles.files[i];
                    addNewFile(
                        file.name!,
                        file.base64Code!,
                        `data:${file.fileType!};base64,${file.base64Code!}`,
                        file.fileType!);
                    files.push(filesFromFileReader[filesFromFileReader.length - 1]);
                }
            }
    
            const postImages = await apiClient.getAllPostImages(postId, '1.0');
    
            let images: FileData[] = [];
    
            if (postImages.images !== undefined && postImages.images.length > 0) {
                for (let i = 0; i < postImages.images.length; i++) {
                    let image = postImages.images[i];
                    addNewFile(
                        '',
                        image.base64Code!,
                        `data:${image.fileType!};base64,${image.base64Code!}`,
                        image.fileType!);
                    images.push(filesFromFileReader[filesFromFileReader.length - 1]);
                }
            }

            let subs = await apiClient.getAllSubscriptions('1.0');
            setSubscriptions(subs.subscriptions!);
            setSubscriptionId(subs.subscriptions![0].id!);
    
            setInputText(post.text!);
            setImageList(images);
            setFileList(files);
        }

        if (postId != '') {
            getPostData();
        }
    }, []);

    return (
        <div className='post'>

            <form onSubmit={handleSubmit}>
                <div className='input_block'>
                    <div className='post_text'>
                        Содержимое:
                    </div>
                    
                    <PostEditorToggleSwitchPanel subscriptionId={subscriptionId} setSubscriptionId={setSubscriptionId} subscriptions={subscriptions}/>
                    <PostEditorTagPanel categories={categories} setCategories={setCategories}/>
                    <PostEditorImagePanel images={imageList} removeImage={removeImage}/>
                    <PostEditorTextArea text={inputText} readFiles={readFiles}/>
                    <PostEditorFilePanel files={fileList} removeFile={removeFile}/>
                    <PostEditorToolbar readFiles={readFiles}/>
                        
                </div>
                        
                <div className='button_panel'>
                    {postId != ''
                    ?
                        <>
                            <input type='button' value='Отмена' className='cancel_button' onClick={handleOnClickCancel}/>
                            <input type='submit' value='Изменить' className='create_button'/>
                        </>
                    :
                        <input type='submit' value='Создать' className='create_button'/>
                    }
                    
                </div>
            </form>
    
        </div>
    );
};