import { useEffect, useState } from 'react';
import SendComment from '../images/send_comment.png';

export default function CommentInputBlock(props: any) {
    const [inputText, setInputText] = useState('');

    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            props.CreateComment(inputText);
            setInputText('');
        }
    };

    useEffect(() => {
        setInputText(props.text);
    }, [props.text]);


    return (
        <div className='comment_input_block'>
                    
            {props.isBeingEdited
            ?
            <div className='comment_input_with_button'>
                <input
                    type='text'
                    name='title'
                    className='comment_input'
                    placeholder='Написать комментарий...'
                    value={inputText}
                    onChange={(event) => setInputText(event.target.value)}
                    onKeyDown={handleKeyDown}
                />
                            
                <button className='cancel_comment_editing_button' onClick={props.CancelEditing}>
                    отмена
                </button>
            </div>
            :
            <input
                type='text'
                name='title'
                className='comment_input'
                placeholder='Написать комментарий...'
                value={inputText}
                onChange={(event) => setInputText(event.target.value)}
                onKeyDown={handleKeyDown}
            />
            }
            <input type='image' className='send_comment_button' src={SendComment} onClick={() => props.CreateComment(inputText)} alt='SendComment'/>
        </div>
    );
};