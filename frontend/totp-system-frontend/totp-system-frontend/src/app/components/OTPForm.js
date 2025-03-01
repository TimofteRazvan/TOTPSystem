"use client";
import { useState } from "react";
import { generateOTP, validateOTP } from "../services/otpService";
import Swal from "sweetalert2";

export default function OTPForm() {
  const [otp, setOTP] = useState("");

  /**
   * Displays a toast notification with a given message, icon, timer, and position.
   * @param {*} html - The HTML content to display in the toast.
   * @param {*} icon - The icon to display ("info", "error", "success" etc.).
   * @param {*} timer - The duration the toast should stay on screen.
   * @param {*} timerProgressBar - Whether to show the timer progress bar (true by default).
   * @param {*} position - The position of the toast on the screen (bottom, center, etc)
   */
  function notifyToast(
    html,
    icon,
    timer,
    timerProgressBar = true,
    position = "center"
  ) {
    Swal.fire({
      toast: true,
      position: position,
      icon: icon,
      html: html,
      showConfirmButton: false,
      timer: timer,
      timerProgressBar: timerProgressBar,
      customClass: {
        popup: "colored-toast",
      },
    });
  }

  /**
   * Handles the OTP generation calling the generateOTP service and displaying the OTP in a toast notification,
   * then calculates the expiration time for the OTP and displays it in the notification.
   */
  const handleGenerateOTP = async () => {
    try {
      const response = await generateOTP();
      const expiresIn =
        new Date(response.expirationTime).getTime() - Date.now();
      notifyToast(
        `<p>OTP: <strong>${response.otp}</strong></p>`,
        "info",
        expiresIn,
        true,
        "bottom"
      );
    } catch (err) {
      notifyToast(`<p>${err.message}</p>`, "error", 3000, true, "center");
    }
  };

  /**
   * Handles the OTP validation by calling validateOTP service and displaying the result in a toast notification.
   * @returns void
   */
  const handleValidateOTP = async () => {
    if (!otp) {
      notifyToast(`<p>OTP is required</p>`, "error", 3000, true, "center");
      return;
    }

    try {
      const response = await validateOTP(otp);
      notifyToast(
        `<p>${response.message}</p>`,
        "success",
        3000,
        true,
        "center"
      );
    } catch (err) {
      notifyToast(`<p>${err.message}</p>`, "error", 3000, true, "center");
    }
  };

  return (
    <div>
      <div>
        <label>OTP:</label>
        <input
          type="text"
          value={otp}
          onChange={(e) => setOTP(e.target.value)}
          placeholder="Enter OTP"
        />
      </div>
      <button onClick={handleGenerateOTP}>Generate OTP</button>
      <button onClick={handleValidateOTP}>Validate OTP</button>
    </div>
  );
}
