import { useContext, useState } from 'react';
import { SearchContext } from './search-provider';

function Search() {
    const [inputText, setInputText] = useState('');

    const { setText } = useContext(SearchContext);

    const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            setText(inputText);
        }
    };

    return (
        <input
            type='search'
            className='header_search'
            placeholder='Поиск'
            value={inputText}
            onChange={(event) => setInputText(event.target.value)}
            onKeyDown={handleKeyDown}
        />
    );
};
export default Search;