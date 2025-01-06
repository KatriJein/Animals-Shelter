import style from './AccountChange.module.css';
import { useSelector, useDispatch } from 'react-redux';
import { useState } from 'react';
import { logout, updateUserInfo, updateUserPassword, updateUserAvatar, deleteUser } from '../../store/userSlice';
import ButtonBack from '../ButtonBack';
import exit from '../../img/exit.svg';
import ava from '../../img/base_avatar.png';
import okay from '../../img/okay.svg';
import { useNavigate } from 'react-router-dom';

export default function AccountChange() {
    const dispatch = useDispatch();
    const user = useSelector((state) => state.user.userInfo);
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        name: user.name || '',
        surname: user.surname || '',
        phone: user.phone || '',
        email: user.email || '',
        oldPassword: '',
        newPassword: ''
    });

    const handleGoBack = () => {
        navigate(-1);
    };

    const handleLogout = () => {
        dispatch(logout());
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSave = async (e) => {
        e.preventDefault();
        const info = {
            name: formData.name,
            surname: formData.surname,
        };

        if (formData.phone) {
            info.phone = formData.phone;
        }
        if (formData.email) {
            info.email = formData.email;
        }

        if (!formData.phone && !formData.email) {
            alert('Необходимо заполнить хотя бы одно поле: телефон или почта.');
            return;
        }

        try {
            await dispatch(updateUserInfo({ userId: user.id, info })).unwrap();
            alert('Данные успешно обновлены');
        } catch (error) {
            alert('Ошибка при обновлении данных: ' + error);
        }
    };

    const handleChangePassword = (e) => {
        e.preventDefault();
        dispatch(updateUserPassword({
            userId: user.id,
            oldPassword: formData.oldPassword,
            newPassword: formData.newPassword
        }))
            .unwrap()
            .then(() => {
                alert('Пароль успешно обновлен')
                setFormData({
                    ...formData,
                    oldPassword: '',
                    newPassword: ''
                });
            })
            .catch((err) => alert(err));
    };

    const handleDeleteAccount = () => {
        dispatch(deleteUser(user.id))
    };

    const handleAvatarChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            dispatch(updateUserAvatar({ userId: user.id, avatarFile: file }))
                .unwrap()
                .catch((err) => alert(err));
        }
    };

    return (
        <div className={style.mainContainer}>
            <div className={style.containerHeader}>
                <div className={style.buttonsContainer}>
                    <ButtonBack onClick={handleGoBack} />
                    <button onClick={handleLogout} className={style.buttonExit}>
                        <span>Выйти из аккаунта</span>
                        <img src={exit} alt="exit" />
                    </button>
                </div>
                <h1 className={style.h1}>Личный кабинет</h1>
            </div>

            <div className={style.containerInfo}>
                <div className={style.avatarContainer}>
                    <img src={user.avatarSrc || ava} alt="аватарка" className={style.avatar} />
                    <label htmlFor="avatar-upload" className={style.changeAvatar}>Изменить</label>
                    <input
                        id="avatar-upload"
                        type="file"
                        accept="image/*"
                        onChange={handleAvatarChange}
                        style={{ display: 'none' }}
                    />
                </div>

                <div className={style.infoChange}>
                    <form className={style.info} onSubmit={handleSave}>
                        <div className={style.personalInfo}>
                            <h2>Личная информация</h2>
                            <div className={style.containerInput}>
                                <label>Имя</label>
                                <input type="text" name="name" value={formData.name} onChange={handleChange} maxLength={13}/>
                            </div>

                            <div className={style.containerInput}>
                                <label>Фамилия</label>
                                <input type="text" name="surname" value={formData.surname} onChange={handleChange} maxLength={20}/>
                            </div>

                        </div>

                        <div className={style.personalInfo}>
                            <h2>Контактная информация</h2>
                            <div className={style.containerInput}>
                                <label>Телефон</label>
                                <input
                                    type="tel"
                                    pattern="^\+7\(\d{3}\)\s\d{3}-\d{2}-\d{2}$"
                                    placeholder="+71111111111"
                                    name="phone"
                                    value={formData.phone}
                                    onChange={handleChange}
                                    maxLength={12}
                                />
                            </div>

                            <div className={style.containerInput}>
                                <label>Почта</label>
                                <input type="email" placeholder="test@mail.ru" name="email" value={formData.email} onChange={handleChange} />
                            </div>

                        </div>

                        <button type="submit" className={style.buttonSave}><span>Сохранить</span><img src={okay} alt="save" /></button>
                    </form>

                    <form className={style.info} onSubmit={handleChangePassword}>
                        <div className={style.personalInfo}>
                            <h2>Пароль</h2>

                            <div className={style.containerInput}>
                                <label className={style.oldPassword}>Старый пароль</label>
                                <input type="password" name="oldPassword" value={formData.oldPassword} onChange={handleChange} />
                            </div>

                            <div className={style.containerInput}>
                                <label className={style.oldPassword}>Новый пароль</label>
                                <input type="password" name="newPassword" value={formData.newPassword} onChange={handleChange} minLength={8}/>
                            </div>
                        </div>

                        <button type="submit" className={style.buttonSave}>Изменить пароль</button>
                    </form>

                    <div className={style.personalInfo}>
                        <h2>Удалить аккаунт</h2>
                        <p className={style.textDelete}>
                            Хотите удалить аккаунт?<br />
                            Удаление аккаунта приведет к потере всей информации, связанной с аккаунтом.
                        </p>
                        <button onClick={handleDeleteAccount} className={style.buttonDelete}>Я хочу удалить аккаунт</button>
                    </div>
                </div>


            </div>
        </div>
    );
}
