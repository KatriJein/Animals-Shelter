import MainInfo from "../components/main/MainInfo";
import Contacts from "../components/contacts/Contacts";
import { Helmet } from "react-helmet-async";

export default function MainPage() {
    return (<>
        <Helmet><title>Приют Лапочка</title></Helmet>
        <MainInfo />
        <Contacts />
    </>
    );
}