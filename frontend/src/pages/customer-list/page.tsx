import React, { FunctionComponent } from "react";
import CustomerList from "../../shared/components/customer-list";

const CustomerListPage: FunctionComponent = () => (
  <div className="container">
    <div className="row py-3">
      <div className="col-12">
        <h1 className="pb-3">Kundenliste</h1>
        <CustomerList />
      </div>
    </div>
  </div>
);

export default CustomerListPage;
