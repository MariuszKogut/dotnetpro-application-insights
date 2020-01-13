import React, { FunctionComponent } from "react";
import CustomerDetails from "../../shared/components/customer-details";

const CustomerDetailsPage: FunctionComponent = () => (
  <div className="container">
    <div className="row py-3">
      <div className="col-12">
        <CustomerDetails />
      </div>
    </div>
  </div>
);

export default CustomerDetailsPage;
