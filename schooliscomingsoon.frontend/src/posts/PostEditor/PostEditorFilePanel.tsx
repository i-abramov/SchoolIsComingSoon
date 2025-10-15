import { useEffect, useState } from 'react';
import FileImg from '../../images/file.png'
import RemoveImg from '../../images/delete.png';
import { FileData } from './PostEditorPage';

function PostEditoFilePanel(props: any) {
    const [fileList, setFileList] = useState<FileData[]>();

    const handleOnClickRemove = (id: number) => {
        props.removeFile(id);
    }

    useEffect(() => {
        setFileList(props.files)
    }, [props.files]);

    return (
        <>
            {
                props.files?.length > 0
                ?
                    <div className='files_panel'>
                        {fileList!.map((file: FileData) => (
                            <div className='file_container'  key={file.name}>
                                <img className='file_icon' alt='file icon' src={FileImg}/>
                                <div className='file_name'><p className='file_name_text'>{file.name}</p></div>
                                <button
                                    className='file_close_button'
                                    type='button'
                                    onClick={() => handleOnClickRemove(file.id)}
                                >
                                    <img
                                        src={RemoveImg}
                                        alt='file close'
                                        className='file_close'
                                    />
                                </button>
                            </div>
                        ))}
                    </div>
                :
                    <div className='files_panel_empty'/>
            }
        </>
    );
};
export default PostEditoFilePanel;