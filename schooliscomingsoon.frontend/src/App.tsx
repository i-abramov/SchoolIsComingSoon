import './App.css';
import { BrowserRouter as Router, Route, Routes, useLocation } from 'react-router-dom';
import SignInOidc from './auth/SigninOidc';
import SignOutOidc from './auth/SignoutOidc';
import Header from './header/header';
import PostList from './posts/PostList';
import PrivateRoute from './routes/private-route';
import SearchContext from './header/search-provider';
import { useState } from 'react';
import { Client, CreatePostDto, CreatePostFileDto, CreatePostImageDto, UpdatePostDto } from './api/api';
import PostByID from './posts/PostByID';
import PostEditorPage, { FormData } from './posts/PostEditor/PostEditorPage';
import RightMenu from './menu/RightMenu';
import ErrorPage from './errors/ErrorPage';
import PurchasePage from './purchase/PurchasePage';

const apiClient = new Client(process.env.REACT_APP_SERVER_URL);

async function CreatePost(formData: FormData) {
  const createPostDto: CreatePostDto = {
    subscriptionId: formData.subscriptionId,
    text: formData.text,
    categories: formData.categories,
  };

  const postId = await apiClient.createPost('1.0', createPostDto);
  await UploadPostAttachments(formData, postId);
  window.location.href = '/index';
}

async function UpdatePost(formData: FormData, postId: string) {
  const updatePostDto: UpdatePostDto = {
    id: postId,
    text: formData.text,
    categories: formData.categories,
    subscriptionId: formData.subscriptionId,
  };

  await apiClient.updatePost('1.0', updatePostDto);

  const images = await apiClient.getAllPostImages(postId, '1.0');
  if (images.images) {
    for (const image of images.images) {
      await apiClient.deletePostImage(image.id, '1.0');
    }
  }

  const files = await apiClient.getAllPostFiles(postId, '1.0');
  if (files.files) {
    for (const file of files.files) {
      await apiClient.deletePostFile(file.id, '1.0');
    }
  }

  await UploadPostAttachments(formData, postId);
  window.location.href = '/index';
}

async function UploadPostAttachments(formData: FormData, postId: string) {
  for (const file of formData.files) {
    const createPostFileDto: CreatePostFileDto = {
      postId,
      name: file.name,
      base64Code: file.base64,
      fileType: file.fileType,
    };
    await apiClient.createPostFile('1.0', createPostFileDto);
  }

  for (const image of formData.images) {
    const createPostImageDto: CreatePostImageDto = {
      postId,
      base64Code: image.base64,
      fileType: image.fileType,
    };
    await apiClient.createPostImage('1.0', createPostImageDto);
  }
}

function AppContent() {
  const [text, setText] = useState('');
  const [editedPostId, setEditedPostId] = useState('');
  const location = useLocation();
  const isPurchasePage = location.pathname === '/purchase';

  async function onSubmit(formData: FormData) {
    CreatePost(formData);
  }

  async function onEdit(formData: FormData) {
    UpdatePost(formData, editedPostId);
  }

  return (
    <div className='App'>
      <SearchContext.Provider value={{ text, setText }}>
        <Header/>

        <div className='page'>
          {isPurchasePage ? (
            <Routes>
                <Route element={<PrivateRoute/>}>
                    <Route
                      path='/purchase'
                      element={<PurchasePage/>}
                    />
                </Route>
            </Routes>
          ) : (
            <>
              <div className='left_menu'></div>

              <div className='main_block'>
                <Routes>
                  <Route element={<PrivateRoute/>}>
                    <Route
                      path='/create-post'
                      element={<PostEditorPage onSubmit={onSubmit} postId=''/>}
                    />
                  </Route>
                  <Route element={<PrivateRoute/>}>
                    <Route
                      path='/edit-post'
                      element={<PostEditorPage onSubmit={onEdit} postId={editedPostId}/>}
                    />
                  </Route>
                  <Route path='/' element={<PostList setPostId={setEditedPostId}/>}/>
                  <Route path='/index' element={<PostList setPostId={setEditedPostId}/>}/>
                  <Route path='/pre-school-education' element={<PostList setPostId={setEditedPostId}/>}/>
                  <Route path='/elementary-grades' element={<PostList setPostId={setEditedPostId}/>}/>
                  <Route path='/information-for-parents' element={<PostList setPostId={setEditedPostId}/>}/>
                  <Route path='/posts/:id' element={<PostByID setPostId={setEditedPostId}/>}/>
                  <Route path='/signout-oidc' element={<SignOutOidc/>}/>
                  <Route path='/signin-oidc' element={<SignInOidc/>}/>
                  <Route path="/home/error" element={<ErrorPage/>}/>
                </Routes>
              </div>

              <RightMenu/>
            </>
          )}
        </div>
      </SearchContext.Provider>
    </div>
  );
}

export default function App() {
  return (
    <Router>
      <AppContent/>
    </Router>
  );
}