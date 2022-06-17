import React, { useEffect, useState } from "react";
import { ReactComponent as LoaderIcon } from "../assets/images/loading.svg";
import "./loader.scss";

export interface LoaderProps<T> {
    promise?: Promise<T>;
    children: (v?: T) => React.ReactNode;
    implicit?: boolean;
}

export function Loader<T>(props: LoaderProps<T>) {
    const [isLoading, setIsLoading] = useState(true);
    const [errorMessage, setErrorMessage] = useState(null);
    const [value, setValue] = useState<T>();

    useEffect(() => {
        if (props.promise !== undefined) {
            setIsLoading(true);
            setErrorMessage(null);

            props.promise
                .then((v) => setValue(v))
                .catch((err) => setErrorMessage(err.toString()))
                .finally(() => setIsLoading(false));
        }
    }, [props.promise]);

    if (errorMessage) {
        return <div className="loader-error">{errorMessage}</div>;
    }
    
    return isLoading && !props.implicit ? (
        <div className="loader">
            <LoaderIcon title="Loading" />
        </div>
    ) : (
        <>{props.children(value)}</>
    );
}