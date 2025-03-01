import { NextResponse } from "next/server";
import { generateOTPResponse } from "../../../services/otpService";

/**
 * Generates an OTP response for the given session ID.
 * @param {*} request - The request object.
 * @returns {NextResponse} - The JSON response containing the OTP (or error).
 */
export async function GET(request) {
  const { searchParams } = new URL(request.url);
  const sessionID = searchParams.get("sessionID");

  if (!sessionID) {
    return NextResponse.json(
      { error: "Session ID is required" },
      { status: 400 }
    );
  }

  const otpResponse = generateOTPResponse(sessionID);
  return NextResponse.json(otpResponse);
}
