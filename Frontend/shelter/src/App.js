import { Provider, useDispatch, useSelector } from 'react-redux';
import { useEffect } from "react";
import { fetchAnimals } from "./store/animalsActions";
import { BrowserRouter as Router, Routes, Route, useLocation } from 'react-router-dom';
import MainPage from "./pages/MainPage";
import CatalogPage from "./pages/CatalogPage";
import LoginPage from "./pages/LoginPage";
import RegistrationPage from "./pages/RegistrationPage";
import AnimalPage from "./pages/AnimalPage";
import Header from './components/header/Header';
import Footer from './components/footer/Footer';
import style from "./App.module.css";
import store from './store/store';
import ScrollToTop from './utils/ScrollToTop';
import AccountPage from './pages/AccountPage';
import FillingData from './components/auth/FillingData';
import UsefulPage from './components/useful/UsefulPage';
import News from './components/news/News';


function App() {
  const dispatch = useDispatch();
  const status = useSelector((state) => state.animals.status);

  useEffect(() => {
    if (status === 'idle') {
      dispatch(fetchAnimals());
    }
  }, [status]);

  return (
    <Provider store={store}>
      <Router>
        <ScrollToTop />
        <Layout>
          <Routes>
            <Route path="/" element={<MainPage />} />
            <Route path="/catalog" element={<CatalogPage />} />
            <Route path="/animal/:id" element={<AnimalPage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegistrationPage />} />
            <Route path="/account" element={<AccountPage />} />
            <Route path="/fillingData" element={<FillingData />} />
            <Route path="/useful" element={<UsefulPage />} />
            <Route path="/news" element={<News />} />
          </Routes>
        </Layout>
      </Router>
    </Provider>
  );
}

function Layout({ children }) {
  const location = useLocation();
  const isAuthPage = location.pathname === '/login' || location.pathname === '/register' || location.pathname === '/fillingData';

  return (
    <div className={style.app}>
      {!isAuthPage && <Header />}
      <div className={style.mainContent}>
        {children}
      </div>
      {!isAuthPage && <Footer />}
    </div>
  );
}

export default App;
