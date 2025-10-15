import LockedPostImg from '../images/locked_post.png';

function LockedPost(props: any) {

    return (
        <div className='post' key={props.post.postDto.id}>
                
            <div className='post_title_container'>
                <ul>
                    <a href={`/posts/${props.post.postDto.id}`}><li className='post_title'>Скоро в школу!</li></a>
                </ul>
            </div>

            <div className='post_date'>{props.post.postDto.creationDate}</div>

            <div className='post_image_block'>
                <img
                    src={LockedPostImg}
                    alt='post preview'
                    className='post_image_preview'
                />
            </div>

            <div className='post_text'>
                Данный пост доступен по подписке «{props.subName}» и выше.
            </div>

            <br></br>
            <br></br>
                          
        </div>
    );
};
export default LockedPost;