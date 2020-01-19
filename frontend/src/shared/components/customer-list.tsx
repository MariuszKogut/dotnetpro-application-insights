import React, { FunctionComponent, useEffect, useMemo, useState } from "react";
import { CustomerClient, ICustomerModel } from "../services/customer-client";
import { LoadingState } from "./loading-state";
import CustomerCard from "./customer-card";

const CustomerList: FunctionComponent = () => {
  const [loadingState, setLoadingState] = useState<LoadingState>(
    LoadingState.Loading
  );
  const [data, setData] = useState<ICustomerModel[]>();
  const [error, setError] = useState<string>();
  const [deletedItems, setDeletedItems] = useState(0);

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
  }, [customerClient, deletedItems]);

  const handleDeleteCustomer = async (id: number) => {
    try {
      await customerClient.delete(id);
      setDeletedItems(deletedItems + 1);
    } catch (e) {
      setError(e);
    }
  };

  switch (loadingState) {
    case LoadingState.Loading:
      return (
        <div className="col-12">
          <div className="spinner-border" role="status">
            <span className="sr-only">Daten werden geladen...</span>
          </div>
        </div>
      );

    case LoadingState.Error:
      return (
        <div className="col-12">
          <div className="alert alert-danger" role="alert">
            Es ist ein Fehler aufgetreten: {error}
          </div>
        </div>
      );

    case LoadingState.NoData:
      return <>Keine Daten vorhanden</>;

    case LoadingState.HasData:
      return (
        <>
          {data &&
            data.map(x => (
              <div className="col-md-3 py-3" key={x.id}>
                <CustomerCard customer={x} onDelete={handleDeleteCustomer} />
              </div>
            ))}
        </>
      );
  }
};

export default CustomerList;
