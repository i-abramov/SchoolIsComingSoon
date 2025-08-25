import { useEffect, useState } from 'react';

export default function PostEditorTextArea(props: any) {

    const [inputText, setInputText] = useState('');

    const handleDrop = (event: any) => {
        event.preventDefault();
        event.stopPropagation();
        props.readFiles(event.dataTransfer.files);
    };

    const handleDragEmpty = (event: any) => {
        event.preventDefault();
        event.stopPropagation();
    };

    useEffect(() => {
        setInputText(props.text);
    }, [props.text]);

    return (
        <textarea
            name='text'
            value={inputText}
            onChange={(event) => setInputText(event.target.value)}
            className='input_richtext'
            onDrop={handleDrop}
            onDragEnter={handleDragEmpty}
            onDragOver={handleDragEmpty}
        ></textarea>
    );
};