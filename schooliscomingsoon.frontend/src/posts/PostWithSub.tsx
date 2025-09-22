import { Client, SubscriptionLookupDto } from '../api/api'; 
import { useContext, useEffect, useState } from 'react';
import { AuthContext } from '../auth/auth-provider';
import Post from './Post';
import LockedPost from './LockedPost';

const apiClient = new Client('https://localhost:44399');

function PostWithSub(props: any) {
    const [subs, setSubs] = useState<SubscriptionLookupDto[]>([]);
    const [postLVL, setPostLVL] = useState<number>(0);
    const [postSubName, setPostSubName] = useState<string>('');
    const [currentLVL, setCurrentLVL] = useState<number>(0);

    const { id, isAuthenticated } = useContext(AuthContext);

    useEffect(() => {
        async function getPostLVL() {
            let sub = await apiClient.getSubscription(props.post.postDto.subscriptionId, '1.0');
            setPostLVL(sub.lvl!);
            setPostSubName(sub.name!);
        }
        getPostLVL();
    }, []);

    useEffect(() => {
        async function getSubs() {
            let subs = await apiClient.getAllSubscriptions('1.0');
            setSubs(subs.subscriptions!);
        }
        getSubs();

        async function getCurSubLVL() {
            let curSub = await apiClient.getCurrentSubscription(id, '1.0');
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
                    <Post post={props.post} role={props.role} setPostId={props.setPostId} isAllCommentsVisible={props.isAllCommentsVisible}/>
            }
        </>
    );
};
export default PostWithSub;