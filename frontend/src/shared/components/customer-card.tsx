import React, { FunctionComponent } from "react";
import { ICustomerModel } from "../services/customer-client";
import { Link } from "react-router-dom";

interface Props {
  customer: ICustomerModel;
  onDelete: (id: number) => void;
}

const CustomerCard: FunctionComponent<Props> = props => {
  const {
    customer: { name, id, location },
    onDelete
  } = props;

  const handleDeleteClick = () => {
    if (id && window.confirm(`Möchten Sie '${name}' wirklich löschen?`)) {
      onDelete(id);
    }
  };

  return (
    <div className="card">
      <div className="card-header font-weight-bold">{name}</div>
      <ul className="list-group list-group-flush">
        <li className="list-group-item">
          <span className="text-muted">#</span> {id}
        </li>
        <li className="list-group-item">
          <span className="text-muted">Location</span> {location}
        </li>
      </ul>
      <div className="card-body d-flex">
        <Link className="btn btn-secondary" to={`/customer/editor/${id}`}>
          Öffnen
        </Link>
        <button className="btn btn-danger ml-auto" onClick={handleDeleteClick}>
          Löschen
        </button>
      </div>
    </div>
  );
};

export default CustomerCard;
