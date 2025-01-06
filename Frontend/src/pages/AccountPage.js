import Account from "../components/account/Account";
import { Helmet } from "react-helmet-async";

export default function AccountPage() {
    return (<>
        <Helmet><title>Личный кабинет</title></Helmet>
        <Account />
    </>
    );
}