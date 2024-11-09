import React, { useState } from 'react';
import styles from './FilterComponent.module.css';
import arrowTop from '../../img/arrow_top.svg';
import arrowDown from '../../img/arrow_down.svg';

export default function FilterComponent(props) {
    const { placeholder, options, selected, onChange, type } = props;
    const [isOpen, setIsOpen] = useState(false);

    const toggleFilter = () => {
        setIsOpen((prev) => !prev);
    };

    const handleChange = (value, isChecked) => {
        if (type === 'checkbox') {
            const newSelected = isChecked
                ? [...selected, value]
                : selected.filter((item) => item !== value);
            onChange(newSelected);
        } else {
            onChange(value);
        }
    };

    return (
        <div className={`${styles.container} ${isOpen ? styles.containerOpen : ''}`}>
            <div className={styles.filterHeader} onClick={toggleFilter}>
                <span>{placeholder}</span>
                <button className={styles.arrowButton}><img src={isOpen ? arrowTop : arrowDown} alt="arrow" /></button>
            </div>

            {isOpen && (
                <div className={styles.filterOptions}>
                    {Object.keys(options).map((key) => (
                        <div key={key} className={styles.filterOption}>
                            <label className={styles.filterLabel}>
                                <input
                                    type={type}
                                    value={key}
                                    checked={type === 'checkbox' ? selected.includes(key) : selected === key}
                                    onChange={(e) =>
                                        handleChange(key, e.target.checked)
                                    }
                                    className={styles.filterCheckbox}
                                />
                                {options[key]}
                            </label>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}
