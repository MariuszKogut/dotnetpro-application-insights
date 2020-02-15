import React, { FunctionComponent } from "react";
import { useParams } from "react-router";
import CustomerDetails from "../../shared/components/customer-details";
import { usePageTracking } from "../../shared/services/use-page-tracking";
import { useTitle } from "react-use";

const CustomerDetailsPage: FunctionComponent = () => {
  const { id } = useParams();
  useTitle(`Kundendetails ${id ? " > " + id : ""}`);
  usePageTracking();

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
