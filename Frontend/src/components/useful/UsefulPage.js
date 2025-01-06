import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchArticles, selectArticles, selectArticlesLoading, selectArticlesError } from "../../store/articlesSlice";
import Useful from "./Useful";
import ArticlesSearch from "./ArticlesSearch";
import ContactsQuestion from "../contactsQuestion/ContactsQuestion";
import style from "./UsefulPage.module.css";
import { findCategoryText } from "../../utils/animalInfo";
import { Helmet } from "react-helmet-async";

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

    useEffect(() => {
        if (isSearchActive) {
            window.scrollTo(0, 0);
        }
    }, [isSearchActive]);

    const handleSearch = (query) => {
        const filtered = articles.filter((article) =>
            article.title.toLowerCase().includes(query.toLowerCase())
        );
        setSearchHeading(query);
        setIsSearch(true);
        setFilteredArticles(filtered);
        setIsSearchActive(true);
    };

    const handleCategoryClick = (category) => {
        const filtered = articles.filter((article) =>
            article.category === category);
        setSearchHeading(findCategoryText(category));
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
                <Helmet><title>Полезная информация</title></Helmet>
                <p className={style.error}>Загрузка...</p>
                <ContactsQuestion />
            </>
        );
    }

    if (error) {
        return (
            <>
                <Helmet><title>Полезная информация</title></Helmet>
                <p className={style.error}>Ошибка сервера</p>
                <ContactsQuestion />
            </>
        );
    }

    return (
        <>
            <Helmet><title>Полезная информация</title></Helmet>
            {isSearchActive ? (
                <ArticlesSearch
                    articles={filteredArticles}
                    isSearch={isSearch}
                    heading={searchHeading}
                    onBack={handleBackClick}
                />
            ) : (
                <Useful
                    popularArticles={articles.slice(0, 5)}
                    onSearch={handleSearch}
                    onCategoryClick={handleCategoryClick}
                />
            )}
            <ContactsQuestion />
        </>
    );
}

export default UsefulPage;
