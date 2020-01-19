import React, {
  FormEvent,
  FunctionComponent,
  useCallback,
  useEffect,
  useMemo,
  useState
} from "react";
import classNames from "classnames";
import { useHistory } from "react-router-dom";
import {
  CustomerClient,
  CustomerModel,
  ValidationProblemDetails
} from "../services/customer-client";
import ProblemDetails, { hasErrors } from "./problem-details";

interface Props {
  id?: number;
}

const CustomerDetails: FunctionComponent<Props> = props => {
  const { id } = props;
  const isInsertMode = id === undefined;

  const history = useHistory();

  const [customer, setCustomer] = useState<CustomerModel>(() => {
    const customer = new CustomerModel();
    customer.id = isInsertMode ? undefined : id;
    customer.name = "";
    customer.location = "";
    return customer;
  });
  const { name, location } = customer;

  const [error, setError] = useState<string>();
  const [problemDetails, setProblemDetails] = useState<
    ValidationProblemDetails
  >();

  const customerClient = useMemo<CustomerClient>(
    () => new CustomerClient("https://localhost:5001"),
    []
  );

  const loadCustomer = useCallback(async () => {
    if (id && id > 0) {
      try {
        const customer = await customerClient.get(id);
        setCustomer(customer);
      } catch (e) {
        setError(e.message);
      }
    }
  }, [id, customerClient]);

  useEffect(() => {
    loadCustomer();
  }, [loadCustomer]);

  const handleBackClick = () => history.goBack();
  const handleSaveClick = async () => {
    try {
      isInsertMode
        ? await customerClient.insert(customer)
        : await customerClient.update(customer);
      history.push("/customer/list");
    } catch (e) {
      if (e instanceof ValidationProblemDetails) {
        setProblemDetails(e);
      } else {
        setError(e.message);
      }
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
          <label htmlFor="Name">Name</label>
          <input
            type="text"
            className={classNames("form-control", {
              "is-invalid": hasErrors("Name", problemDetails).length > 0
            })}
            id="Name"
            aria-describedby="NameHelp"
            value={name}
            onChange={handleNameChanged}
          />
          <small id="NameHelp" className="form-text text-muted">
            Name des Unternehmens, z. B. Microsoft
          </small>
          <ProblemDetails fieldName="Name" problemDetails={problemDetails} />
        </div>
        <div className="form-group">
          <label htmlFor="Location">Location</label>
          <input
            type="text"
            className={classNames("form-control", {
              "is-invalid": hasErrors("Location", problemDetails).length > 0
            })}
            id="Location"
            aria-describedby="LocationHelp"
            value={location}
            onChange={handleLocationChanged}
          />
          <small id="LocationHelp" className="form-text text-muted">
            Sitz des Unternehmens, z. B. USA
          </small>
          <ProblemDetails
            fieldName="Location"
            problemDetails={problemDetails}
          />
        </div>
      </form>
    </>
  );
};

export default CustomerDetails;
