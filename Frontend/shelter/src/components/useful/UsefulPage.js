import React, { useState } from "react";
import Useful from "./Useful";
import ArticlesSearch from "./ArticlesSearch";
import ContactsQuestion from "../contactsQuestion/ContactsQuestion";

const Information = [
  { id: "1", heading: "Подходит ли Вам питомец из приюта?", text: "Текст 1" },
  { id: "2", heading: "Как выбрать питомца?", text: "Текст 2" },
  { id: "3", heading: "Какие нужны документы для того, чтоб приютить питомца?", text: "Текст 3" },
];

function UsefulPage() {
  const [isSearchActive, setIsSearchActive] = useState(false);
  const [filteredArticles, setFilteredArticles] = useState([]);
  const [searchHeading, setSearchHeading] = useState("");
  const [isSearch, setIsSearch] = useState(false);
  const popularArticles = [...Information];

  const handleSearch = (query) => {
    const filtered = Information.filter((article) =>
      article.heading.toLowerCase().includes(query.toLowerCase())
    );
    setSearchHeading(query);
    setIsSearch(true);
    setFilteredArticles(filtered);
    setIsSearchActive(true);
  };

  const handleCategoryClick = (category) => {
    const filtered = Information.filter((article) =>
      article.heading.toLowerCase().includes(category.toLowerCase())
    );
    console.log('vze')
    setSearchHeading(category);
    setIsSearch(false);
    setFilteredArticles(filtered);
    setIsSearchActive(true);
  };

  const handleBackClick = () => {
    setIsSearchActive(false);
    setIsSearch(false);
    setFilteredArticles([]);
    setSearchHeading("");
  };

  return (
    <>
      {isSearchActive ? (
        <ArticlesSearch
          articles={filteredArticles}
          isSearch={isSearch}
          heading={searchHeading}
          onBack={handleBackClick}
        />
      ) : (
        <Useful
          popularArticles={popularArticles}
          onSearch={handleSearch}
          onCategoryClick={handleCategoryClick}
        />
      )}
      <ContactsQuestion />
    </>
  );
}

export default UsefulPage;
