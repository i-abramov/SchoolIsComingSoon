import { useState } from 'react';
import CommentList from '../comments/CommentList';
import commentsImg from '../images/comments.png';
import Reactions from '../reactions/reactions';

function PostFooter(props: any) {
    const [commentsCount, setCommentsCount] = useState('');

    return (
        <>
            <div className='post_footer'>
                <div className='post_footer_left'>
                    <Reactions postId={props.postId}/>
                </div>
                                
                <div className='post_footer_left'>
                    <img className='post_footer_icon' alt='post footer icon' src={commentsImg}/>
                    <div className='post_footer_text'>{commentsCount !== '0' ? commentsCount : ''}</div>
                </div>
            </div>

            <CommentList
                postId={props.postId}
                commentsCount={commentsCount}
                setCommentsCount={setCommentsCount}
                isAllCommentsVisible={props.isAllCommentsVisible}
            />
        </>
    );
};
export default PostFooter;