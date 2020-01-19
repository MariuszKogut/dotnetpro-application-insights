import React, { FunctionComponent } from "react";
import { useParams } from "react-router";
import CustomerDetails from "../../shared/components/customer-details";

const CustomerDetailsPage: FunctionComponent = () => {
  const { id } = useParams();

  return (
    <div className="container">
      <div className="row py-3">
        <div className="col-12">
          <CustomerDetails id={id ? parseInt(id) : undefined} />
        </div>
      </div>
    </div>
  );
};

export default CustomerDetailsPage;
