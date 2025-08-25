import { useEffect, useState } from 'react';
import RemoveImg from '../../images/delete.png';
import { FileData } from './PostEditorPage';

export default function PostEditorImagePanel(props: any) {
    const [imageList, setImageList] = useState<FileData[]>([]);

    const handleOnClickRemove = (id: number) => {
        props.removeImage(id);
    }

    useEffect(() => {
        setImageList(props.images);
    }, [props.images]);

    return (
        <div className={imageList.length > 0 ? 'image_panel' : 'image_panel_empty'}>
            {imageList?.map((image: FileData) => (
                <div className='image_block' key={image.id}>
                    <img
                        src={image.url}
                        className='image_preview'
                    />
                    <button
                        className='image_close_button'
                        type='button'
                        onClick={() => handleOnClickRemove(image.id)}
                    >
                        <img
                            src={RemoveImg}
                            className='image_close'
                        />
                    </button>
                </div>
            ))}
        </div>
    );
};