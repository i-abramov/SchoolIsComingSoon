import AddFileImg from '../../images/file.png'
import AddImageImg from '../../images/image.png'

export default function PostEditorToolbar(props: any) {
    const handleOnChange = (event: any) => {
        event.preventDefault();
        props.readFiles(event.target.files);

        const input = event.target as HTMLInputElement;
        input.value = '';
    };

    return (
        <form className='post_editor_toolbar'>

            <label
                htmlFor='image-loader-button'
                className='custom-button'
            >
                <img className='add_image_button' alt='add button' src={AddImageImg}/>
            </label>
            <input
                id='image-loader-button'
                type='file'
                className='upload-button'
                onChange={handleOnChange}
            />

            <label
                htmlFor='file-loader-button'
                className='custom-button'
            >
                <img className='add_file_button' alt='add file button' src={AddFileImg}/>
            </label>
            <input
                id='file-loader-button'
                type='file'
                className='upload-button'
                onChange={handleOnChange}
            />
                                   
        </form>
    );
};