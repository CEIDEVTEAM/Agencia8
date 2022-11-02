const apiURL = process.env.REACT_APP_API_URL;

export const testUrl = `${apiURL}/Test`;
export const authUrl = `${apiURL}/login`;




export const userUrl = `${apiURL}/user`;
export const userUrlTotalRecords = `${apiURL}/user/usersTotalRecords`;
export const urerUrlEdit = `${apiURL}/user/edit`;
export const urlNewUser = `${apiURL}/user/AddUser`;

export const candidateUrl = `${apiURL}/candidate`;
export const urlNewcandidate = `${apiURL}/candidate/addCandidate`;
export const urlCandidateStep = `${apiURL}/candidate/step`;
export const urlCandidatePostStep = `${apiURL}/candidate/addStep`;
export const urlCandidateDecision = `${apiURL}/candidate/recomendedDecision`;
export const urlNeighborhood = `${apiURL}/candidate/neighborhoods`;

export const dependentUrl = `${apiURL}/dependent`;
export const urlDeleteDependent = `${apiURL}/dependent/deleteDependent`;
export const exCandidateDependent = `${apiURL}/dependent/exCandidateDependent`;
export const externalDependentUrl = `${apiURL}/dependent/externalDependent`;
export const getExternalDependentUrl = `${apiURL}/dependent/getExternalDependent`;
export const proccessExternalsUrl = `${apiURL}/dependent/processExternals`;

export const decitionParamUrl = `${apiURL}/decitionParam`;


export const projectionParamUrl = `${apiURL}/projectionParam`;
export const urlNewParam = `${apiURL}/projectionParam/addProjectionParam`;

export const conceptsUrl = `${apiURL}/concept`;
export const urlParamsOptions = `${apiURL}/concept/projectionParam`;


