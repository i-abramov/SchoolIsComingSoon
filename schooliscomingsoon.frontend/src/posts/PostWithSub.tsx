import { Client } from '../api/api'; 
import { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../auth/auth-provider';
import Post from './Post';
import LockedPost from './LockedPost';

const apiClient = new Client(process.env.REACT_APP_SERVER_URL);

function PostWithSub(props: any) {
    const [postLVL, setPostLVL] = useState<number>(-1);
    const [postSubName, setPostSubName] = useState<string>('');
    const [currentLVL, setCurrentLVL] = useState<number>(0);

    const { isAuthenticated } = useContext(AuthContext);

    useEffect(() => {
        async function getPostLVL() {
            let sub = await apiClient.getSubscription(props.post.postDto.subscriptionId, '1.0');
            setPostLVL(sub.lvl!);
            setPostSubName(sub.name!);
        }
        getPostLVL();
    }, [props.post.postDto.subscriptionId]);

    useEffect(() => {
        async function getCurSubLVL() {
            let curSub = await apiClient.getCurrentSubscription('1.0');
            let sub = await apiClient.getSubscription(curSub.subscriptionId!, '1.0');
            setCurrentLVL(sub.lvl!);
        }

        if (isAuthenticated) {
            getCurSubLVL();
        }

    }, [isAuthenticated]);

    return (
        <>
            {
                postLVL > 0
                ?
                    <>
                        {
                            isAuthenticated && postLVL <= currentLVL
                            ?
                                <Post post={props.post} role={props.role} setPostId={props.setPostId} isAllCommentsVisible={props.isAllCommentsVisible}/>
                            :
                                <LockedPost post={props.post} subName={postSubName}/>
                        }
                    </>
                :
                    <>
                        {
                            postLVL === -1
                            ?
                            <></>
                            :
                            <Post post={props.post} role={props.role} setPostId={props.setPostId} isAllCommentsVisible={props.isAllCommentsVisible}/>
                        }
                    </>
                    
            }
        </>
    );
};
export default PostWithSub;