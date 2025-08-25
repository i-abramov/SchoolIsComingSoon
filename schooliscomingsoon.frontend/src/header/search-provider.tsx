import React, { useState, createContext } from 'react';

type SearchContextType = {
    text: string;
    setText: (text: string) => void;
};
  
export const SearchContext = createContext<SearchContextType>({
    text: '',
    setText: () => { }
});

type SearchProviderProps = {
    children?: React.ReactNode;
};

const SearchProvider = ({ children }: SearchProviderProps) => {
    const [text, setText] = useState('');
  
    return (
        <SearchContext.Provider value={{ text, setText}}>
            {children}
        </SearchContext.Provider>
    );
};
  
export default SearchContext;