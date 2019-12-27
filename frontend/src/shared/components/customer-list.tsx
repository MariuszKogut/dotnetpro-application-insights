import React, { FunctionComponent, useEffect, useMemo, useState } from "react";
import { CustomerClient, ICustomerModel } from "../services/customer-client";
import { LoadingState } from "./loading-state";

const CustomerList: FunctionComponent = () => {
  const [loadingState, setLoadingState] = useState<LoadingState>(
    LoadingState.Loading
  );
  const [data, setData] = useState<ICustomerModel[]>();
  const [error, setError] = useState<string>();

  const customerClient = useMemo<CustomerClient>(
    () => new CustomerClient("https://localhost:5001"),
    []
  );

  useEffect(() => {
    const loadCustomer = async () => {
      setLoadingState(LoadingState.Loading);
      try {
        const result = await customerClient.getAll();
        setData(result);
        setLoadingState(
          result.length > 0 ? LoadingState.HasData : LoadingState.NoData
        );
      } catch (e) {
        setError(e.message);
        setLoadingState(LoadingState.Error);
      }
    };

    loadCustomer();
  }, [customerClient]);

  switch (loadingState) {
    case LoadingState.Loading:
      return (
        <div className="spinner-border" role="status">
          <span className="sr-only">Daten werden geladen...</span>
        </div>
      );

    case LoadingState.Error:
      return (
        <div className="alert alert-danger" role="alert">
          Es ist ein Fehler aufgetreten: {error}
        </div>
      );

    case LoadingState.NoData:
      return <>Keine Daten vorhanden</>;

    case LoadingState.HasData:
      return (
        <ul>
          {data &&
            data.map(x => (
              <li key={x.id}>
                {x.id}, {x.name}, {x.location}
              </li>
            ))}
        </ul>
      );
  }
};

export default CustomerList;
