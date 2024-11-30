import Header from "../components/header/Header";
import MainInfo from "../components/main/MainInfo";
import Footer from "../components/footer/Footer";
import Contacts from "../components/contacts/Contacts";
import News from "../components/news/News";
import Account from "../components/account/Account";
import { logout } from "../store/userSlice";
import { useDispatch } from "react-redux";

export default function MainPage() {
    const dispatch = useDispatch();

    // const logoutAcc = () => {
    //     dispatch(logout());
    // }
    return (<>
    {/* <Account /> */}
    {/* <News /> */}
        <MainInfo />
        <Contacts />
        {/* <button onClick={logoutAcc}>Выйти</button> */}
    </>
    );
}