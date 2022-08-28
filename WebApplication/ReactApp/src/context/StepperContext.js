import React from "react";
import { createContext, useContext, useState } from "react";

const StepperContext = createContext({ data: "", setData: null });

export function UseContextProvider({ children }) {
  const [data, setData] = useState("");

  return (
    <StepperContext.Provider value={{ data, setData }}>
      {children}
    </StepperContext.Provider>
  );
}

export function useStepperContext() {
  const { data, setData } = useContext(StepperContext);

  return { data, setData };
}
