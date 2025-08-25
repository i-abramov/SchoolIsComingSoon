import logoutImg from '../images/logout.png';

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
    return (
    <div className='header_auth_panel'>
		<a className='username'>{props.name}</a>
		<input type='image' className='logout_button' src={logoutImg} onClick={props.onClick}/>
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
        <a href='/index'><li className='menu_item'>Главная</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/pre-school-education'><li className='menu_item'>Дошкольное образование</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/elementary-grades'><li className='menu_item'>Начальные классы</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/information-for-parents'><li className='menu_item'>Информация для родителей</li></a>
    </ul>)
}
  
function AdminHButtons() {
    return(
    <ul className='header_buttons_panel'>
        <a href='/index'><li className='menu_item'>Главная</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/pre-school-education'><li className='menu_item'>Дошкольное образование</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/elementary-grades'><li className='menu_item'>Начальные классы</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/information-for-parents'><li className='menu_item'>Информация для родителей</li></a>
        <a><li className='menu_item'>|</li></a>
        <a href='/create-post'><li className='menu_item'>Новый пост</li></a>
    </ul>)
}