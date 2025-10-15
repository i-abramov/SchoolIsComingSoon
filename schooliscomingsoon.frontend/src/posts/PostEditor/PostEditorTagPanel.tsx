import { useEffect, useState } from 'react';
import BlueTag from '../../images/blue_tag.png'
import GreenTag from '../../images/green_tag.png'
import OrangeTag from '../../images/orange_tag.png'

export default function PostEditorTagPanel(props: any) {
    const [blueTagChecked, setBlueTagChecked] = useState<boolean>(false);
    const [greenTagChecked, setGreenTagChecked] = useState<boolean>(false);
    const [orangeTagChecked, setOrangeTagChecked] = useState<boolean>(false);

    const onChangeCategories = () => {
        let categories = '';
        if (blueTagChecked) {
            categories += 'Дошкольное образование\n'
        } if (greenTagChecked) {
            categories += 'Начальные классы\n'
        } if (orangeTagChecked) {
            categories += 'Информация для родителей\n'
        }
        props.setCategories(categories);
    }

    const handleOnClickBlueTag = () => {
        setBlueTagChecked(!blueTagChecked);
    };

    const handleOnClickGreenTag = () => {
        setGreenTagChecked(!greenTagChecked);
    };

    const handleOnClickOrangeTag = () => {
        setOrangeTagChecked(!orangeTagChecked);
    };

    useEffect(() => {
        onChangeCategories();
    }, [blueTagChecked, greenTagChecked, orangeTagChecked]);

    useEffect(() => {  
        if (props.categories.length > 0) {
            if (props.categories.includes('Дошкольное образование')) {
                setBlueTagChecked(true);
            } if (props.categories.includes('Начальные классы')) {
                setGreenTagChecked(true);
            } if (props.categories.includes('Информация для родителей')) {
                setOrangeTagChecked(true);
            } 
        }
    }, [props.categories]);

    return (
        <div className='tag_panel'>
            <button
                id='blue-tag'
                type='button'
                className={blueTagChecked ? 'tag tag_active_blue' : 'tag tag_inactive'}
                onClick={handleOnClickBlueTag}
            >
                <img
                    src={BlueTag}
                    alt='tag'
                    className='tag_image'
                />
                дошкольное образование
            </button>

            <button
                id='green-tag'
                type='button'
                className={greenTagChecked ? 'tag tag_active_green' : 'tag tag_inactive'}
                onClick={handleOnClickGreenTag}
            >
                <img
                    src={GreenTag}
                    alt='tag'
                    className='tag_image'
                />
                начальные классы
            </button>

            <button
                id='orange-tag'
                type='button'
                className={orangeTagChecked ? 'tag tag_active_orange' : 'tag tag_inactive'}
                onClick={handleOnClickOrangeTag}
            >
                <img
                    src={OrangeTag}
                    alt='tag'
                    className='tag_image'
                />
                информация для родителей
            </button>
        </div>
    );
};