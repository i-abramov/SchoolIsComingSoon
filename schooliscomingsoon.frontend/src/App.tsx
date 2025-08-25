import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SignInOidc from './auth/SigninOidc';
import SignOutOidc from './auth/SignoutOidc';
import Header from './header/header'
import PostList from './posts/PostList';
import PrivateRoute from './routes/private-route'
import SearchContext from './header/search-provider';
import { useState } from 'react';
import { Client, CreatePostDto, CreatePostFileDto, CreatePostImageDto, UpdatePostDto } from './api/api';
import PostByID from './posts/PostByID';
import PostEditorPage, { FormData } from './posts/PostEditor/PostEditorPage';

const apiClient = new Client('https://localhost:44399');

async function CreatePost(formData: FormData) {
    const createPostDto: CreatePostDto = {
        text: formData.text,
        categories: formData.categories
    };

    const postId = await apiClient.createPost('1.0', createPostDto);

    await UploadPostAttachments(formData, postId);

    window.location.href = '/index';
}

async function UpdatePost(formData: FormData, postId: string) {
    const updatePostDto: UpdatePostDto = {
        id: postId,
        text: formData.text,
        categories: formData.categories
    };

    await apiClient.updatePost('1.0', updatePostDto);

    const images = await apiClient.getAllPostImages(postId, '1.0');

    if (images.images) {
        for (let i = 0; i < images.images.length; i++) {
            await apiClient.deletePostImage(images.images[i].id, '1.0');
        }
    }

    const files = await apiClient.getAllPostFiles(postId, '1.0');

    if (files.files) {
        for (let i = 0; i < files.files.length; i++) {
            await apiClient.deletePostFile(files.files[i].id, '1.0');
        }
    }

    await UploadPostAttachments(formData, postId);

    window.location.href = '/index';
}

async function UploadPostAttachments(formData: FormData, postId: string) {
    if (formData.files.length > 0) {
        for (let i = 0; i < formData.files.length; i++) {
            const createPostFileDto: CreatePostFileDto = {
                postId: postId,
                name: formData.files[i].name,
                base64Code: formData.files[i].base64,
                fileType: formData.files[i].fileType
            };
        
            await apiClient.createPostFile('1.0', createPostFileDto);
        }
    }

    if (formData.images.length > 0) {
        for (let i = 0; i < formData.images.length; i++) {
            const createPostImageDto: CreatePostImageDto = {
                postId: postId,
                base64Code: formData.images[i].base64,
                fileType: formData.images[i].fileType
            };
        
            await apiClient.createPostImage('1.0', createPostImageDto);
        }
    }
}

export default function App() {
    const [text, setText] = useState('');
    const [editedPostId, setEditedPostId] = useState('');

    async function onSubmit(formData: FormData) {
        CreatePost(formData);
    }

    async function onEdit(formData: FormData) {
        UpdatePost(formData, editedPostId);
    }

    return (
        <Router>
            <div className='App'>
                <SearchContext.Provider value={{text, setText}}>
                    <Header/>
    
                    <div className='page'>
                        <div className='main_block container'>
                        
                                <Routes>
                                    <Route element={<PrivateRoute/>}>
                                        <Route path='/create-post' element={<PostEditorPage onSubmit={onSubmit} postId=''/>}/>
                                    </Route>
                                    <Route element={<PrivateRoute/>}>
                                        <Route path='/edit-post' element={<PostEditorPage onSubmit={onEdit} postId={editedPostId}/>}/>
                                    </Route>
                                    <Route path='/' element={<PostList setPostId={setEditedPostId}/>}/>
                                    <Route path='/index' element={<PostList setPostId={setEditedPostId}/>}/>
                                    <Route path='/pre-school-education' element={<PostList setPostId={setEditedPostId}/>}/>
                                    <Route path='/elementary-grades' element={<PostList setPostId={setEditedPostId}/>}/>
                                    <Route path='/information-for-parents' element={<PostList setPostId={setEditedPostId}/>}/>
                                    <Route path='/posts/:id' element={<PostByID setPostId={setEditedPostId}/>}/>
                                    <Route path='/signout-oidc' element={<SignOutOidc/>}/>
                                    <Route path='/signin-oidc' element={<SignInOidc/>} />
                                </Routes>
                        
                        </div>
                    </div>
                </SearchContext.Provider>
            </div>
        </Router>
    );
}