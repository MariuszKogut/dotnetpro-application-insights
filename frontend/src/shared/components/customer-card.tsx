import React, { FunctionComponent } from "react";
import { ICustomerModel } from "../services/customer-client";
import { Link } from "react-router-dom";

interface Props {
  customer: ICustomerModel;
}

const CustomerCard: FunctionComponent<Props> = props => {
  const {
    customer: { name, id, location }
  } = props;

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
      <div className="card-body">
        <Link to={`/customer/editor/${id}`}>Öffnen</Link>
      </div>
    </div>
  );
};

export default CustomerCard;
