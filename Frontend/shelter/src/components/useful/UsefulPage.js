import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchArticles, selectArticles, selectArticlesLoading, selectArticlesError } from "../../store/articlesSlice";
import Useful from "./Useful";
import ArticlesSearch from "./ArticlesSearch";
import ContactsQuestion from "../contactsQuestion/ContactsQuestion";
import style from "./UsefulPage.module.css";

function UsefulPage() {
    const dispatch = useDispatch();
    const articles = useSelector(selectArticles);
    const isLoading = useSelector(selectArticlesLoading);
    const error = useSelector(selectArticlesError);

    const [isSearchActive, setIsSearchActive] = useState(false);
    const [filteredArticles, setFilteredArticles] = useState([]);
    const [searchHeading, setSearchHeading] = useState("");
    const [isSearch, setIsSearch] = useState(false);

    useEffect(() => {
        dispatch(fetchArticles());
    }, [dispatch]);

    const handleSearch = (query) => {
        const filtered = articles.filter((article) =>
            article.heading.toLowerCase().includes(query.toLowerCase())
        );
        setSearchHeading(query);
        setIsSearch(true);
        setFilteredArticles(filtered);
        setIsSearchActive(true);
    };

    const handleCategoryClick = (category) => {
        const filtered = articles.filter((article) =>
            article.heading.toLowerCase().includes(category.toLowerCase())
        );
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

    if (isLoading) {
        return (
            <>
                <p className={style.error}>Загрузка...</p>
                <ContactsQuestion />
            </>
        );
    }

    if (error) {
        return (
            <>
                <p className={style.error}>Ошибка сервера</p>
                <ContactsQuestion />
            </>
        );
    }

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
                    popularArticles={articles}
                    onSearch={handleSearch}
                    onCategoryClick={handleCategoryClick}
                />
            )}
            <ContactsQuestion />
        </>
    );
}

export default UsefulPage;
