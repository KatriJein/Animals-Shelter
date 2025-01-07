import Registration from "../components/auth/Registration";
import { Helmet } from "react-helmet-async";

export default function RegistrationPage() {
    return (
        <>
            <Helmet><title>Регистрация</title></Helmet>
            <Registration />
        </>
    )
}