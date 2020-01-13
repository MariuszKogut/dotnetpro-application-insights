import React, { FormEvent, FunctionComponent, useMemo, useState } from "react";
import { useHistory } from "react-router-dom";
import { CustomerClient, CustomerModel } from "../services/customer-client";

const CustomerDetails: FunctionComponent = () => {
  const history = useHistory();

  const [customer, setCustomer] = useState<CustomerModel>(() => {
    const customer = new CustomerModel();
    customer.name = "";
    customer.location = "";
    return customer;
  });
  const { name, location } = customer;

  const [error, setError] = useState<string>();

  const customerClient = useMemo<CustomerClient>(
    () => new CustomerClient("https://localhost:5001"),
    []
  );

  const handleBackClick = () => history.goBack();
  const handleSaveClick = async () => {
    try {
      await customerClient.insert(customer);
      history.push("/customer/list");
    } catch (e) {
      setError(e.message);
    }
  };

  const handleNameChanged = (e: FormEvent<HTMLInputElement>) => {
    const changedCustomer = new CustomerModel();
    changedCustomer.init({ ...customer, name: e.currentTarget.value });
    setCustomer(changedCustomer);
  };

  const handleLocationChanged = (e: FormEvent<HTMLInputElement>) => {
    const changedCustomer = new CustomerModel();
    changedCustomer.init({ ...customer, location: e.currentTarget.value });
    setCustomer(changedCustomer);
  };

  return (
    <>
      <h1 className="pb-3">Kunde {name} hinzufügen</h1>
      <hr />
      <button className="btn btn-primary btn-lg mr-3" onClick={handleSaveClick}>
        Speichern
      </button>
      <button className="btn btn-secondary btn-lg" onClick={handleBackClick}>
        Zurück
      </button>
      <hr />

      {error && error.length > 0 && (
        <div className="alert alert-danger" role="alert">
          Es ist ein Fehler aufgetreten: {error}
        </div>
      )}

      <form>
        <div className="form-group">
          <label htmlFor="name">Name</label>
          <input
            type="text"
            className="form-control"
            id="name"
            aria-describedby="nameHelp"
            value={name}
            onChange={handleNameChanged}
          />
          <small id="nameHelp" className="form-text text-muted">
            Name des Unternehmens, z. B. Microsoft
          </small>
        </div>
        <div className="form-group">
          <label htmlFor="location">Location</label>
          <input
            type="text"
            className="form-control"
            id="location"
            aria-describedby="locationHelp"
            value={location}
            onChange={handleLocationChanged}
          />
          <small id="locationHelp" className="form-text text-muted">
            Sitz des Unternehmens, z. B. USA
          </small>
        </div>
      </form>
    </>
  );
};

export default CustomerDetails;
