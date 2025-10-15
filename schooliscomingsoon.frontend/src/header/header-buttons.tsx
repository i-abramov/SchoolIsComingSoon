import { useEffect, useRef, useState } from 'react';
import logoutImg from '../images/logout.png';
import settingsImg from '../images/settings.png';

export function LoginButton(props: any) {
    return (
        <ul className='header_auth_panel'>
            <button className='auth_button' onClick={props.onClick}>
                Войти
            </button>
        </ul>
    );
}
  
export function LogoutButton(props: any) {
    const [showMenu, setShowMenu] = useState(false);
    const menuRef = useRef<HTMLDivElement>(null);

    function toggleMenu() {
        setShowMenu(prev => !prev);
    }

    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
                setShowMenu(false);
            }
        };
        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    return (
        <div className='header_auth_panel'>
            <span className='username'>{props.name}</span>

            <div className="settings-container" ref={menuRef}>
                <input
                    type='image'
                    alt='settings button'
                    className='settings_button'
                    src={settingsImg}
                    onClick={toggleMenu}
                />
                {showMenu && (
                    <div className="dropdown-menu">
                        <button className="dropdown-item" onClick={props.onClickSettings}>
                            Сменить пароль
                        </button>
                    </div>
                )}
            </div>

            <input
                type='image'
                alt='logout button'
                className='logout_button'
                src={logoutImg}
                onClick={props.onClickLogout}
            />
        </div>
    );
}

export function MenuButtons(props: any) {
    if (props.role === 'Owner' ||
        props.role === 'Admin') {
        return <AdminHButtons/>;
    }
    return <UserHButtons/>;
}

function UserHButtons() {
    return(
    <ul className='header_buttons_panel'>
        <a className='menu_item_container' href='/index'><li className='menu_item'>Главная</li></a>
        <a className='menu_item_container'><li className='menu_item_splitter'>|</li></a>
        <a className='menu_item_container' href='/pre-school-education'><li className='menu_item'>Дошкольное образование</li></a>
        <a className='menu_item_container'><li className='menu_item_splitter'>|</li></a>
        <a className='menu_item_container' href='/elementary-grades'><li className='menu_item'>Начальные классы</li></a>
        <a className='menu_item_container'><li className='menu_item_splitter'>|</li></a>
        <a className='menu_item_container' href='/information-for-parents'><li className='menu_item'>Информация для родителей</li></a>
    </ul>)
}
  
function AdminHButtons() {
    return(
    <ul className='header_buttons_panel'>
        <a className='menu_item_container' href='/index'><li className='menu_item'>Главная</li></a>
        <a className='menu_item_container'><li className='menu_item_splitter'>|</li></a>
        <a className='menu_item_container' href='/pre-school-education'><li className='menu_item'>Дошкольное образование</li></a>
        <a className='menu_item_container'><li className='menu_item_splitter'>|</li></a>
        <a className='menu_item_container' href='/elementary-grades'><li className='menu_item'>Начальные классы</li></a>
        <a className='menu_item_container'><li className='menu_item_splitter'>|</li></a>
        <a className='menu_item_container' href='/information-for-parents'><li className='menu_item'>Информация для родителей</li></a>
        <a className='menu_item_container'><li className='menu_item_splitter'>|</li></a>
        <a className='menu_item_container' href='/create-post'><li className='menu_item'>Новый пост</li></a>
    </ul>)
}