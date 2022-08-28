import React, { useState } from 'react';
import { ErrorMessage, Field, Form, Formik } from 'formik';
import { Button } from '@windmill/react-ui'
import Stepper from "../stepper/Stepper";

export const sleep = ms => new Promise(resolve => setTimeout(resolve, ms));

export default function Wizard({ children, initialValues, onSubmit, stepsNames }) {
  const [stepNumber, setStepNumber] = useState(0);
  const steps = React.Children.toArray(children);
  const [snapshot, setSnapshot] = useState(initialValues);

  const step = steps[stepNumber];
  const totalSteps = steps.length;
  const isLastStep = stepNumber === totalSteps - 1;

  const next = values => {
    setSnapshot(values);
    setStepNumber(Math.min(stepNumber + 1, totalSteps - 1));
  };

  const previous = values => {
    setSnapshot(values);
    setStepNumber(Math.max(stepNumber - 1, 0));
  };

  const handleSubmit = async (values, bag) => {
    if (step.props.onSubmit) {
      await step.props.onSubmit(values, bag);
    }
    if (isLastStep) {
      return onSubmit(values, bag);
    } else {
      bag.setTouched({});
      next(values);
    }
  };

  return (
    <Formik
      initialValues={snapshot}
      onSubmit={handleSubmit}
      validationSchema={step.props.validationSchema}
    >
      {formik => (
        <Form>
          <div className="horizontal container mt-5 ">
            <Stepper steps={stepsNames} currentStep={stepNumber + 1} />            
          </div>
          <br/>
          <div>{step}</div>
          
          <div className="grid md:grid-cols-6 md:gap-6">
            {stepNumber > 0 && (
              <Button onClick={() => previous(formik.values)} type="button">
                Atrás
              </Button>
            )}

            <Button disabled={formik.isSubmitting} type="submit">
              {isLastStep ? 'Ingresar' : 'Siguiente'}
            </Button>

          </div>

        </Form>
      )}
    </Formik>
  );
};