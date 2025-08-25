import { useContext } from 'react';
import DeleteButton from '../images/delete.png';
import EditButton from '../images/edit.png';
import { AuthContext } from '../auth/auth-provider';

export default function Comment(props: any) {
    const { role, id } = useContext(AuthContext);

    const handleOnClickReply = () => {
        props.replyToComment(`${props.comment.firstName}, `);
    }

    return(
        <div className='comment' key={props.comment.id}>
            <div className='comment_head_container'>
                <div className='comment_username'>{`${props.comment.firstName} ${props.comment.lastName}`}</div>
                {
                    role === 'Owner' || role === 'Admin' || props.comment.userId === id
                    ?
                    <div className='comment_buttons_container'>
                        <input type='image' className='comment_edit_button' src={EditButton} onClick={() => props.editComment(props.comment.id!, props.comment.text)} alt='edit'/>
                        <input type='image' className='comment_delete_button' src={DeleteButton} onClick={() => props.deleteComment(props.comment.id!)} alt='delete'/>
                    </div>
                    :
                    <></>
                }
            </div>
            <div className='comment_text'>{props.comment.text}</div>
            <div className='comment_date'>{props.comment.creationDate}</div>
            <button className='reply_button' onClick={handleOnClickReply}>
                ответить
            </button>
        </div>
    );
}