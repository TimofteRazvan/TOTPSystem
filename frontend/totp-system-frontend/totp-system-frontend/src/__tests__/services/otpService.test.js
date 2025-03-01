import { describe, it, expect, beforeEach, afterEach, vi } from "vitest";
import { generateOTP, validateOTP } from "../../app/services/otpService";

describe("otpService", () => {
  beforeEach(() => {
    global.fetch = vi.fn();
  });

  afterEach(() => {
    vi.resetAllMocks();
  });

  it("should throw an error if OTP is empty", async () => {
    const mockErrorResponse = { error: "OTP cannot be empty" };
    fetch.mockResolvedValueOnce({
      ok: false,
      json: () => Promise.resolve(mockErrorResponse),
    });

    await expect(validateOTP("")).rejects.toThrow("OTP cannot be empty");
  });

  it("should generate an OTP", async () => {
    const mockResponse = {
      otp: "123456",
      expirationTime: "2023-10-25T12:00:00Z",
    };
    fetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve(mockResponse),
    });

    const result = await generateOTP();
    expect(result).toEqual(mockResponse);
    expect(fetch).toHaveBeenCalledWith(
      "https://localhost:8080/api/OTP/generate",
      {
        credentials: "include",
      }
    );
  });

  it("should throw an error if OTP generation fails", async () => {
    fetch.mockResolvedValueOnce({
      ok: false,
      json: () => Promise.resolve({ error: "Failed to generate OTP" }),
    });

    await expect(generateOTP()).rejects.toThrow("Failed to generate OTP");
  });

  it("should validate an OTP", async () => {
    const mockResponse = { message: "OTP is valid" };
    fetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve(mockResponse),
    });

    const result = await validateOTP("123456");
    expect(result).toEqual(mockResponse);
    expect(fetch).toHaveBeenCalledWith(
      "https://localhost:8080/api/OTP/validate",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify({ otp: "123456" }),
      }
    );
  });

  it("should throw an error if OTP validation fails", async () => {
    fetch.mockResolvedValueOnce({
      ok: false,
      json: () => Promise.resolve({ error: "Failed to validate OTP" }),
    });

    await expect(validateOTP("123456")).rejects.toThrow(
      "Failed to validate OTP"
    );
  });
});
