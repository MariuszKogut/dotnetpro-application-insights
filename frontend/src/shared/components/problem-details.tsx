import React, { FunctionComponent, useMemo } from "react";
import { ValidationProblemDetails } from "../services/customer-client";

export const hasErrors = (
  fieldName: string,
  problemDetails?: ValidationProblemDetails
) => {
  if (
    problemDetails &&
    problemDetails.errors &&
    problemDetails.errors.hasOwnProperty(fieldName)
  ) {
    return problemDetails.errors[fieldName];
  }

  return [];
};

interface Props {
  fieldName: string;
  problemDetails?: ValidationProblemDetails;
}

const ProblemDetails: FunctionComponent<Props> = props => {
  const { fieldName, problemDetails } = props;

  const errors = useMemo(() => hasErrors(fieldName, problemDetails), [
    fieldName,
    problemDetails
  ]);

  return (
    <>
      {errors && errors.length > 0 && (
        <div className="invalid-feedback">
          {errors.map((x, i) => (
            <span key={i}>{x}</span>
          ))}
        </div>
      )}
    </>
  );
};

export default ProblemDetails;
