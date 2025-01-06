import Login from "../components/auth/Login";
import { Helmet } from "react-helmet-async";

export default function LoginPage() {
    return (
        <>
            <Helmet><title>Вход</title></Helmet>
            <Login />
        </>
    )
}