import Catalog from "../components/catalog/Catalog";
import Contacts from "../components/contacts/Contacts";
import { Helmet } from "react-helmet-async";

export default function CatalogPage() {
    return (<>
        <Helmet><title>Каталог животных</title></Helmet>
        <Catalog />
        <Contacts />
    </>
    );
}