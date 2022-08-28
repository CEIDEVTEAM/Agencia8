import React from 'react'

const ErrorsEffect = (props) => {
    const effect = () => {
        if (props.formik.submitCount > 0 && !props.formik.isValid) {
          props.onSubmissionError();
        }
      };
      React.useEffect(effect, [props.formik.submitCount]);
      return null;
}

export default ErrorsEffect



