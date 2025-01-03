import { useSelector } from 'react-redux';
import { Navigate, useLocation } from 'react-router';
import { selectUserInfo } from '../../store/userSlice';

export const ProtectedRoute = ({
    onlyUnAuth,
    onlyAdmin,
    children
}) => {
    const location = useLocation();
    const from = location.state?.from || { pathname: '/' };
    const user = useSelector(selectUserInfo);
    
    if (!onlyUnAuth && !user) {
        return <Navigate replace to='/login' state={{ from: location }} />;
    }

    if (onlyUnAuth && user) {
        return <Navigate replace to={from} />;
    }

    if (onlyAdmin && !user.isAdmin) {
        return <Navigate replace to={from} />;
    }

    return children;
};
