import React, { FunctionComponent } from "react";
import CustomerList from "../../shared/components/customer-list";
import { Link } from "react-router-dom";
import { usePageTracking } from "../../shared/services/use-page-tracking";
import { useTitle } from "react-use";

const CustomerListPage: FunctionComponent = () => {
  useTitle("Kundenliste");
  usePageTracking();

  return (
    <div className="container">
      <div className="row py-3">
        <div className="col-12">
          <h1 className="pb-3">Kundenliste</h1>
          <hr/>
          <Link to="/customer/new" className="btn btn-primary btn-lg">
            Kunde hinzufügen
          </Link>
          <hr/>
        </div>
      </div>
      <div className="row">
        <CustomerList/>
      </div>
    </div>
  );
};

export default CustomerListPage;
