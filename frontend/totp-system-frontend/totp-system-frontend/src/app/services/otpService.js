const BACKEND_URL = "http://localhost:8000/api/OTP";

/**
 * Generates a new OTP by sending a request to the backend. The OTP will be returned along with its lifetime.
 * The session cookie is included in the request for tracking the session that will be used for verifying the OTP.
 * @returns {Promise<Object>} - The OTP response with the OTP and its lifetime.
 * @throws {Error} - If the generate request fails or the server gives back an error.
 */
export const generateOTP = async () => {
  const response = await fetch(`${BACKEND_URL}/generate`, {
    credentials: "include",
  });
  if (!response.ok) {
    const errorData = await response.json();
    throw new Error(errorData.error || "Failed to generate OTP");
  }
  return response.json();
};

/**
 * Validates the OTP by sending a POST request to the backend.
 * The session cookie is included in the response to track the user's session.
 * @param {*} otp - The OTP to validate.
 * @returns {Promise<Object>} - If the OTP validation fails or the server gives back an error.
 * @throws {Error} - If the OTP validation fails or the server gives back an error.
 */
export const validateOTP = async (otp) => {
  const response = await fetch(`${BACKEND_URL}/validate`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    credentials: "include",
    body: JSON.stringify({ otp }),
  });
  if (!response.ok) {
    const errorData = await response.json();
    throw new Error(errorData.error || "Failed to validate OTP");
  }
  return response.json();
};
