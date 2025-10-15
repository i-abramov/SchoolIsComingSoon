import { useContext, useEffect, useState } from 'react';
import { Client, CommentLookupDto, CreateCommentDto, UpdateCommentDto } from '../api/api';
import { AuthContext } from '../auth/auth-provider';
import CommentInputBlock from './CommentInputBlock';
import Comment from './Comment';
import { useNavigate } from 'react-router-dom';

const apiClient = new Client(process.env.REACT_APP_SERVER_URL);

function CommentList(props: any) {
    const [comments, setComments] = useState<CommentLookupDto[] | undefined>(undefined);
    const [inputText, setInputText] = useState('');
    const [isBeingEdited, setBeingEdited] = useState(false);
    const [editedTextId, setEditedTextId] = useState('');

    const navigate = useNavigate();

    let isFirstComment = true;

    const { isAuthenticated } = useContext(AuthContext);

    const onClickShowMoreComments = () => {
        navigate(`/posts/${props.postId}`);
    }

    const replyToComment = (text: string) => {
        setInputText(text);
    }

    async function getComments() {
        const commentListVm = await apiClient.getAllComments(props.postId, '1.0');
        if (commentListVm.comments !== undefined) {
            if (!props.isAllCommentsVisible && commentListVm.comments?.length > 3) {
                setComments(commentListVm.comments?.slice(0, 3));
            }
            else {
                setComments(commentListVm.comments);
            }
        }
        props.setCommentsCount(commentListVm.comments?.length.toString());
    }

    async function CreateComment(text: string) {
        if (isBeingEdited) {
            const updateCommentDto: UpdateCommentDto = {
                id: editedTextId,
                postId: props.postId,
                text: text
            };
            await apiClient.updateComment('1.0', updateCommentDto);
        }
        else {
            const createCommentDto: CreateCommentDto = {
                postId: props.postId,
                text: text
            };
            await apiClient.createComment('1.0', createCommentDto);
        }

        setBeingEdited(false);
        setInputText('');
        
        getComments();
    }

    async function deleteComment(id: string) {
        await apiClient.deleteComment(id, '1.0');
        getComments();
    }

    async function editComment(id: string, text: string) {
        setInputText(text);
        setEditedTextId(id);
        setBeingEdited(true);
    }

    function CancelEditing() {
        setInputText('');
        setBeingEdited(false);
    }

    useEffect(() => {
        getComments();
    }, []);

    return (
        <>
            {comments?.length! > 0 ? (
                <>
                    <div className='splitter'/>
                    <div className='comments_block'>
                        {comments?.map((comment, index) => (
                            <div className='comment_wrapper' key={comment.id}>
                                {index > 0 && <div className='comment_splitter'></div>}
                                <Comment
                                    comment={comment}
                                    deleteComment={deleteComment}
                                    editComment={editComment}
                                    replyToComment={replyToComment}
                                />
                            </div>
                        ))}

                        {!props.isAllCommentsVisible && props.commentsCount > 3 && (
                            <button
                                className='show_more_comments_button'
                                onClick={onClickShowMoreComments}
                            >
                                Показать больше комментариев
                            </button>
                        )}

                        {isAuthenticated && (
                            <CommentInputBlock
                                text={inputText}
                                isBeingEdited={isBeingEdited}
                                CancelEditing={CancelEditing}
                                CreateComment={CreateComment}
                            />
                        )}
                    </div>
                </>
            ) : (
                <>
                    {isAuthenticated && (
                        <div className='comments_block'>
                            <div className='splitter' />
                            <CommentInputBlock
                                text={inputText}
                                isBeingEdited={isBeingEdited}
                                CancelEditing={CancelEditing}
                                CreateComment={CreateComment}
                            />
                        </div>
                    )}
                </>
            )}
        </>
        
    );
};
export default CommentList;