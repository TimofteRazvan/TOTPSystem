import { NextResponse } from "next/server";
import { validateOTP } from "../../../services/otpService";

/**
 * Validates the OTP for the given session ID.
 * @param {*} request - The POST request containing both the session ID and OTP.
 * @returns {NextResponse} - The JSON response indicating whether the OTP is valid or not
 */
export async function POST(request) {
  const { sessionID, otp } = await request.json();

  if (!sessionID || !otp) {
    return NextResponse.json(
      { error: "Session ID and OTP are required" },
      { status: 400 }
    );
  }

  const isValid = validateOTP(sessionID, otp);
  if (!isValid) {
    return NextResponse.json(
      { error: "OTP is invalid or expired" },
      { status: 400 }
    );
  }

  return NextResponse.json({ message: "OTP is valid" });
}
