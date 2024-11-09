import shelter from "./img/shelter.png";
// import style from "./App.module.css";
import Header from "./components/header/Header";
import MainInfo from "./components/main/MainInfo";
import Catalog from "./components/catalog/Catalog";
import PageAnimal from "./components/pageAnimal/pageAnimal";
import { Provider, useDispatch, useSelector } from 'react-redux';
import { useEffect } from "react";
import { fetchAnimals } from "./store/animalsActions";
import UsefulPage from "./components/useful/UsefulPage";
import Login from "./components/auth/Login";
import Registration from "./components/auth/Registration";

function App() {
  const dispatch = useDispatch();
  const status = useSelector((state) => state.animals.status);
  const animals = useSelector((state) => state.animals.animals);

  useEffect(() => {
    if (status === 'idle') {
      dispatch(fetchAnimals());
    }
  }, [status]);

  console.log(animals, status);

  return (
    <>
    {/* <Registration /> */}
      <Header />
      {/* <Login /> */}
      {/* <UsefulPage /> */}
      <Catalog />
      {/* <MainInfo />  */}
      {/* <PageAnimal /> */}
    </>
  );
}

export default App;
